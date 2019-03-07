using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {

    private void InstallSignals() {
        SignalBusInstaller.Install(Container);
        // Send the remaining time of the game every "GameConfig.GameTimerSettings.timerUpdateStep"
        Container.DeclareSignal<SignalGameTimer>();
        // Transmit the total player points
        Container.DeclareSignal<SignalSessionPoints>();
        // Reset all figures on screen and return them back to pool
        Container.DeclareSignal<SignalDisablePlayFigures>();
        // Play figure send points when player tap on it or it destroyed by life time
        Container.DeclareSignal<SignalOnFigurePoints>();

    }

    public override void InstallBindings() {
        InstallSignals();
    }
}