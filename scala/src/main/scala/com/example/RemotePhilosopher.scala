package com.example

import akka.actor._
import scala.concurrent.duration._
import scala.util.Random
import Fork._

object RemotePhilosopher {
  def props(name: String, first: ActorSelection, second: ActorSelection): Props =
    Props(new RemotePhilosopher(name, first, second))
}

class RemotePhilosopher(name: String, first: ActorSelection, second: ActorSelection) extends Actor {
  import context.dispatcher

  var hasFirst = false
  var hasSecond = false
  var meals = 0

  override def preStart(): Unit = think()

  def think(): Unit = {
    val delay = Random.between(300, 1000).millis
    //println(s"$name is thinking for ${delay.toMillis} ms...")
    context.system.scheduler.scheduleOnce(delay, self, "eat")
  }

  def tryToEat(): Unit = {
    if (!hasFirst) first ! PickUp
    else if (!hasSecond) second ! PickUp
  }

  def receive: Receive = {
    case "eat" =>
      tryToEat()

    case PickedUp =>
      if (!hasFirst) {
        hasFirst = true
        second ! PickUp
      } else if (!hasSecond) {
        hasSecond = true
        eat()
      }

    case Busy =>
      val retry = Random.between(500, 600).millis
      println(s"$name found a fork busy. Retrying in ${retry.toMillis} ms...")
      context.system.scheduler.scheduleOnce(retry, self, "eat")

    case "done" =>
      first ! PutDown
      second ! PutDown
      hasFirst = false
      hasSecond = false
      think()
  }

  def eat(): Unit = {
    meals += 1
    val delay = Random.between(300,600).millis
    println(s"$name is eating. Total meals: $meals. Eating for ${delay.toMillis} ms...")
    context.system.scheduler.scheduleOnce(delay, self, "done")
  }
}
