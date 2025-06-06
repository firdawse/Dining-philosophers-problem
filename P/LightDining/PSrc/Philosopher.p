type PhilosopherInit = (left: Fork, right: Fork, id: int, reverse: bool);

machine Philosopher {
  var left: Fork;
  var right: Fork;
  var id: int;
  var reverse: bool;
  var forksHeld: int;

  start state Init {
    entry (init: PhilosopherInit) {
      left = init.left;
      right = init.right;
      id = init.id;
      reverse = init.reverse;

      if (reverse) {
        goto TryRightFirst;
      } else {
        goto TryLeftFirst;
      }
    }
  }

  state TryLeftFirst {
    entry { send left, ePickUp, this; }

    on ForkGranted goto TryRight;
    on ForkBusy do { send left, ePickUp, this; }  // retry
  }

  state TryRightFirst {
    entry { send right, ePickUp, this; }

    on ForkGranted goto TryLeft;
    on ForkBusy do { send right, ePickUp, this; }  // retry
  }

  state TryLeft {
    entry { send left, ePickUp, this; }

    on ForkGranted goto Eating;
    on ForkBusy do { send left, ePickUp, this; }  // retry
  }

  state TryRight {
    entry { send right, ePickUp, this; }

    on ForkGranted goto Eating;
    on ForkBusy do { send right, ePickUp, this; }  // retry
  }

  state Eating {
    entry {
      forksHeld = 0;
      send left, ePutDown, this;
      send right, ePutDown, this;
    }

    on ForkReleased do {
      forksHeld = forksHeld + 1;
      if (forksHeld == 2) {
        if (reverse) {
          goto TryRightFirst;
        } else {
          goto TryLeftFirst;
        }
      }
    }
  }
}
