machine TestV1 {
  start state Init {
    entry { SetupSystem(5, false); }
  }
}

machine TestV2 {
  start state Init {
    entry { SetupSystem(5, true); }
  }
}

// n: number of philosophers
// reverseOne: if true, philosopher 0 picks up right fork first
fun SetupSystem(n: int, reverseOne: bool) {
  var forks: map[int, Fork];
  var i: int;
  var left : Fork;
  var right : Fork;
  var rev : bool;

  i=0;

  // Create forks
  while (i < n) {
    forks[i] = new Fork();
    i = i + 1;
  }

  // Create philosophers
  i = 0;
  while (i < n) {
    left = forks[i];
    right = forks[(i + 1) % n];
    rev = reverseOne && (i == 0);
    new Philosopher((left = left, right = right, id = i, reverse = rev));
    i = i + 1;
  }
}

