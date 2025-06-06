package com.example

import akka.actor._
import com.typesafe.config.ConfigFactory

object PhilosopherApp extends App {
  val config = ConfigFactory.load("philosopher.conf")
  val system = ActorSystem("DiningSystem", config)

  for (i <- 0 until 5) {
    val firstPath =
      if (i == 4) s"akka://DiningSystem@127.0.0.1:2551/user/Fork${(i + 1) % 5}"
      else s"akka://DiningSystem@127.0.0.1:2551/user/Fork$i"

    val secondPath =
      if (i == 4) s"akka://DiningSystem@127.0.0.1:2551/user/Fork$i"
      else s"akka://DiningSystem@127.0.0.1:2551/user/Fork${(i + 1) % 5}"

    val firstFork = system.actorSelection(firstPath)
    val secondFork = system.actorSelection(secondPath)

    system.actorOf(RemotePhilosopher.props(s"Philosopher $i", firstFork, secondFork), s"Philosopher$i")
  }
}
