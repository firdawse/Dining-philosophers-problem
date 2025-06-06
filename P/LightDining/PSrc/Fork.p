event ePickUp: Philosopher;
event ePutDown: Philosopher;
event ForkGranted: Philosopher;
event ForkReleased: Philosopher;
event ForkBusy: Philosopher;

machine Fork {
  var holder: Philosopher;

  start state Free {
    on ePickUp do (p: Philosopher) {
      holder = p;
      send holder, ForkGranted, p;
      goto Held;
    }
  }

  state Held {
    on ePutDown do (p: Philosopher) {
      assert p == holder;
      send holder, ForkReleased, p;
      goto Free;
    }

    on ePickUp do (p: Philosopher) {
      send p, ForkBusy, p;  // Notify philosopher to retry
    }
  }
}
