using BLEExample.Services.BLE.SharedCore.Builder;
using BLEExample.Services.BLE.SharedImplementation.Contracts;
using BLEExample.Services.BLE.SharedImplementation.EventArgs;
using BLEExample.Services.BLE.SharedImplementation.Exceptions;
using Microsoft.Maui.Controls;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace BLEExample.Services.BLE.SharedImplementation
{
    /// <summary>
    /// Base handler holding shared functionality of BLE interaction and defining
    /// abstract/virtual methods that must be handled in the platform implementation
    /// </summary>
    public abstract class BLEHandlerBase : IBLEHandler
    {
        /// <summary>
        /// Limit the scan time
        /// </summary>
        private CancellationTokenSource _scanCancellationTokenSource;

        /// <summary>
        /// Define scanning state of the handler.
        /// This field may be accessed by different threads in certain circumstances.
        /// </summary>
        private volatile bool _isScanning;

        /// <summary>
        /// Invoked when a peripheral advertisment is received
        /// </summary>
        public event EventHandler<BLEPeripheralEventArgs> PeripheralAdvertised;

        /// <summary>
        /// Invoked when a peripheral is discovered
        /// </summary>
        public event EventHandler<BLEPeripheralEventArgs> PeripheralDiscovered;

        /// <summary>
        /// Invoked when a peripheral is connected to successfully
        /// </summary>
        public event EventHandler<BLEPeripheralEventArgs> PeripheralConnected;

        /// <summary>
        /// Invoked when a peripheral loses connection
        /// </summary>
        public event EventHandler<BLEPeripheralErrorEventArgs> PeripheralConnectionLost;

        /// <summary>
        /// Occurs when a device has been disconnected.
        /// </summary>
        public event EventHandler<BLEPeripheralEventArgs> PeripheralDisconnected;

        /// <summary>
        /// Invoked when a peripheral errors trying to connect
        /// </summary>
        public event EventHandler<BLEPeripheralErrorEventArgs> PeripheralConnectionError;

        /// <summary>
        /// Invoked when the allowed scan time has elapsed
        /// </summary>
        public event EventHandler ScanTimeoutElapsed;

        /// <summary>
        /// Is the handler scanning currently
        /// </summary>
        public bool IsScanning
        {
            get => _isScanning;
            private set => _isScanning = value;
        }

        /// <summary>
        /// Timeout for scanning for BLE peripherals. 
        /// </summary>
        public int ScanTimeout { get; set; } = 25000;

        /// <summary>
        /// Thread safe dictionary used to store all connected perhiperals.
        /// </summary>
        public ConcurrentDictionary<string, IBLEPeripheral> ConnectedPeripheralsStore { get; } = new ConcurrentDictionary<string, IBLEPeripheral>();

        /// <summary>
        /// Immutable List of all connected perhiperals.
        /// </summary>
        public IReadOnlyList<IBLEPeripheral> ConnectedPeripherals =>
                                                ConnectedPeripheralsStore.Values.ToList();

        /// <summary>
        /// Thread safe dictionary used to store all discovered perhiperals.
        /// </summary>
        protected ConcurrentDictionary<Guid, IBLEPeripheral> DiscoveredPeripheralsStore { get; } = new ConcurrentDictionary<Guid, IBLEPeripheral>();

        /// <summary>
        /// Immutable List of all discovered perhiperals.
        /// </summary>
        public virtual IReadOnlyList<IBLEPeripheral> DiscoveredPeripherals =>
                                                DiscoveredPeripheralsStore.Values.ToList();


        /// <summary>
        /// Start scanning for any BLE perhiperals using the native implementation
        /// </summary>
        /// <param name="allowDuplicatesKey">Do not allow duplicate peripherals</param>
        /// <param name="cancellationToken">Cancel the scan after X ms</param>
        /// <returns></returns>
        public async Task StartScanningForDevicesAsync(bool allowDuplicatesKey = false,
                                                       CancellationToken cancellationToken = default)
        {
            if (IsScanning) { return; }

            try
            {
                IsScanning = true;
                _scanCancellationTokenSource = new CancellationTokenSource();

                DiscoveredPeripheralsStore.Clear();

                using (cancellationToken.Register(() => _scanCancellationTokenSource?.Cancel()))
                {
                    await StartScanningNativeAsync(allowDuplicatesKey, _scanCancellationTokenSource.Token);
                    await Task.Delay(ScanTimeout, _scanCancellationTokenSource.Token);
                    ScanTimeoutElapsed?.Invoke(this, new System.EventArgs());
                }
            }
            catch (TaskCanceledException ex)
            {
                //TODO implement exception handling service TASKCancelled during scan
            }
            catch (Exception ex)
            {
                //TODO implement exception handling service Task threw general exception
                throw;
            }
            finally
            {
                ResetSharedAndNativeScanState();
            }
        }

        /// <summary>
        /// Stops scanning for BLE peripherals by requesting cancellation of the token passed to the native implementation of scan.
        /// </summary>
        public void StopScanningForDevicesAsync()
        {
            if (_scanCancellationTokenSource != null && !_scanCancellationTokenSource.IsCancellationRequested)
            {
                _scanCancellationTokenSource.Cancel();
            }
        }

        /// <summary>
        /// Connects to the <paramref name="device"/>.
        /// </summary>
        /// <param name="peripheral">BLE object to connect to.</param>
        /// <param name="connectParameters">Connection parameters. Contains platform specific parameters needed to achieved connection. The default value is None.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns>A task that represents the asynchronous read operation. The Task will finish after the device has been connected successfuly.</returns>
        public async Task ConnectToPeripheralAsync(IBLEPeripheral peripheral, BLEConnectionParameters connectParameters = default, CancellationToken cancellationToken = default)
        {
            try
            {
                if (peripheral == null)
                {
                    throw new ArgumentNullException(nameof(peripheral));
                }

                if (peripheral.ConnectionState == BLEPeripheralConnectionState.Connected)
                {
                    return;
                }

                using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
                {
                    await TaskEventBuilder.BuildTask<bool, EventHandler<BLEPeripheralEventArgs>, EventHandler<BLEPeripheralErrorEventArgs>>(
                        execute: () =>
                        {
                            ConnectToPeripheralNativeAsync(peripheral, connectParameters, cts.Token);
                        },

                        getCompleteHandler: (complete, reject) => (sender, args) =>
                        {
                            if (args.Peripheral.Id == peripheral.Id)
                            {
                                complete(true);
                            }
                        },
                        subscribeComplete: handler => PeripheralConnected += handler,
                        unsubscribeComplete: handler => PeripheralConnected -= handler,

                        getRejectHandler: reject => (sender, args) =>
                        {
                            if (args.Peripheral?.Id == peripheral.Id)
                            {
                                reject(new BLEPeripheralConnectionException((Guid)args.Peripheral?.Id, args.Peripheral?.Name,
                                    args.ErrorMessage));
                            }
                        },

                        subscribeReject: handler => PeripheralConnectionError += handler,
                        unsubscribeReject: handler => PeripheralConnectionError -= handler,
                        token: cts.Token);
                }
            }
            catch (TaskCanceledException)
            {

            }
            catch (ArgumentNullException ex)
            {
                //TODO implement error logger on connect task peripheral null
                //TODO implement alert dialog and pass message
            }
            catch (BLEPeripheralConnectionException ex)
            {
                //TODO implement error logger on connect task peripheral exception
                //TODO implement alert dialog and pass message
            }
            catch (Exception ex)
            {
                //TODO implement error logger on connect task general exception
            }
        }

        /// <summary>
        /// Disconnects from the target IPeripheral
        /// </summary>
        /// <param name="peripheral">BLE object that was connected</param>
        public Task DisconnectPeripheralAsync(IBLEPeripheral peripheral)
        {

            if (!ConnectedPeripherals.Contains(peripheral))
            {
                return Task.FromResult(false);
            }

            return TaskEventBuilder.BuildTask<bool, EventHandler<BLEPeripheralEventArgs>, EventHandler<BLEPeripheralErrorEventArgs>>(
               execute: () => DisconnectNative(peripheral),

               getCompleteHandler: (complete, reject) => ((sender, args) =>
               {
                   if (args.Peripheral.Id == peripheral.Id)
                   {
                       complete(true);
                   }
               }),
               subscribeComplete: handler => PeripheralDisconnected += handler,
               unsubscribeComplete: handler => PeripheralDisconnected -= handler,

               getRejectHandler: reject => ((sender, args) =>
               {
                   if (args.Peripheral.Id == peripheral.Id)
                   {
                       reject(new Exception("Disconnect operation exception"));
                   }
               }),
               subscribeReject: handler => PeripheralConnectionError += handler,
               unsubscribeReject: handler => PeripheralConnectionError -= handler);
        }


        /// <summary>
        /// Handle discovery of a new peripheral.
        /// </summary>
        public void HandleDiscoveredPeripheral(IBLEPeripheral peripheral)
        {
            PeripheralAdvertised?.Invoke(this, new BLEPeripheralEventArgs { Peripheral = peripheral });

            if (DiscoveredPeripheralsStore.ContainsKey(peripheral.Id))
            {
                return;
            }

            DiscoveredPeripheralsStore[peripheral.Id] = peripheral;
            PeripheralDiscovered?.Invoke(this, new BLEPeripheralEventArgs { Peripheral = peripheral });
        }

        /// <summary>
        /// Handle connection of a new peripheral.
        /// </summary>
        public void HandleConnectedPeripheral(IBLEPeripheral peripheral)
        {
            PeripheralConnected?.Invoke(this, new BLEPeripheralEventArgs { Peripheral = peripheral });
        }

        /// <summary>
        /// Handle disconnection of a peripheral.
        /// </summary>
        public void HandleDisconnectedPeripheral(bool disconnectRequested, IBLEPeripheral peripheral)
        {
            try
            {
                if (disconnectRequested)
                {
                    PeripheralDisconnected?.Invoke(this, new BLEPeripheralEventArgs { Peripheral = peripheral });
                }
                else
                {
                    PeripheralConnectionLost?.Invoke(this, new BLEPeripheralErrorEventArgs { Peripheral = peripheral, ErrorMessage = "Connection Lost" });

                    DiscoveredPeripheralsStore.TryRemove(peripheral.Id, out _);
                }
            }
            catch (ArgumentNullException ex)
            {
                //TODO exception handling
            }
            catch (Exception ex)
            {
                //TODO exception handling
            }
        }

        /// <summary>
        /// Handle connection failure.
        /// </summary>
        public void HandleConnectionFail(IBLEPeripheral peripheral, string errorMessage)
        {
            PeripheralConnectionError?.Invoke(this, new BLEPeripheralErrorEventArgs
            {
                Peripheral = peripheral,
                ErrorMessage = errorMessage
            });
        }

        /// <summary>
        /// Native implementation definition of scanning for peripherals.
        /// To be implemented by inheriting platform class.
        /// </summary>
        protected abstract Task StartScanningNativeAsync(bool allowDuplicatesKey, CancellationToken scanCancellationToken);
        /// <summary>
        /// Stopping the native scan.
        /// To be implemented by inheriting platform class.
        /// </summary>
        protected abstract void StopScanningNative();
        /// <summary>
        /// Native implementation of connecting to the peripheral.
        /// To be implemented by inheriting platform class.
        /// </summary>
        protected abstract Task ConnectToPeripheralNativeAsync(IBLEPeripheral peripheral, BLEConnectionParameters connectParameters, CancellationToken cancellationToken);
        /// <summary>
        /// Native implementation of native implementation of disconnect peripherals.
        /// To be implemented by inheriting platform class.
        /// </summary>
        protected abstract void DisconnectNative(IBLEPeripheral peripheral);

        /// <summary>
        /// Stops the native scan if the shared implementation stops.
        /// Resets the cancellation token.
        /// Resets the isScanning value to false.
        /// </summary>
        private void ResetSharedAndNativeScanState()
        {
            StopScanningNative();

            if (_scanCancellationTokenSource != null)
            {
                _scanCancellationTokenSource.Dispose();
                _scanCancellationTokenSource = null;
            }

            IsScanning = false;
        }

    }
}
