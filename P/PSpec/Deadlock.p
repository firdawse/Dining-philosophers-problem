
spec NoDeadlock observes ForkGranted, ForkReleased {
  var holders: set[Philosopher];

  start state Monitoring {
    on ForkGranted do (p: Philosopher) {
      holders += (p);
      assert sizeof(holders) < 5, "Deadlock risk: All 5 philosophers hold a fork!";
    }

    on ForkReleased do (p: Philosopher) {
      holders -= p;
    }
  }
}
