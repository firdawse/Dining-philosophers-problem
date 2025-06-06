package com.example

import akka.actor.{Actor, Props}

object Fork {
  case object PickUp
  case object PutDown
  case object PickedUp
  case object Busy

  def props(): Props = Props(new Fork)
}

class Fork extends Actor {
  import Fork._

  var inUse = false

  def receive: Receive = {
    case PickUp =>
      if (!inUse) {
        inUse = true
        sender() ! PickedUp
      } else {
        sender() ! Busy
      }

    case PutDown =>
      inUse = false
  }
}
