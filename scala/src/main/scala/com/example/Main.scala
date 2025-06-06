package com.example

import akka.actor._

object Main extends App {
  val system = ActorSystem("DiningPhilosophers")

  // Read number of philosophers from args(0)
  val N = if (args.nonEmpty) args(0).toInt else {
    println("Usage: sbt \"runMain com.example.Main <numPhilosophers>\"")
    sys.exit(1)
  }

  require(N >= 2, "There must be at least 2 philosophers.")

  // Create N forks
  val forks = (0 until N).map(i => system.actorOf(Fork.props(), s"Fork$i"))
  
  // Create N philosophers (asymmetric to avoid deadlock)
  for (i <- 0 until N) {
    val first = if (i == N - 1) forks((i + 1) % N) else forks(i)
    val second = if (i == N - 1) forks(i) else forks((i + 1) % N)
    system.actorOf(Philosopher.props(s"Philosopher $i", first, second), s"Philosopher$i")
  }
  
  // Uncomment this block to force a deadlock (all pick left first)
 /* 
  for (i <- 0 until N) {
    val left = forks(i)
    val right = forks((i + 1) % N)
    system.actorOf(Philosopher.props(s"Philosopher $i", left, right), s"Philosopher$i")
  }
  */
}
