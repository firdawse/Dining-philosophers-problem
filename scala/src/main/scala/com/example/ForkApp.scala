package com.example

import akka.actor._
import com.typesafe.config.ConfigFactory

object ForkApp extends App {
  val config = ConfigFactory.load("fork.conf") 
  val system = ActorSystem("DiningSystem", config)

  for (i <- 0 until 5) {
    system.actorOf(Fork.props(), s"Fork$i")
  }

  println("✅ Forks started on port 2551.")
}
