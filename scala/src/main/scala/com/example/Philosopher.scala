package com.example

import akka.actor._
import scala.concurrent.duration._
import Fork._
import scala.util.Random

object Philosopher {
  def props(name: String, firstFork: ActorRef, secondFork: ActorRef): Props =
    Props(new Philosopher(name, firstFork, secondFork))
}

class Philosopher(name: String, firstFork: ActorRef, secondFork: ActorRef) extends Actor {
  import context.dispatcher

  var hasFirst = false
  var hasSecond = false
  var meals = 0

  override def preStart(): Unit = think()

  def think(): Unit = {
    val delay = Random.between(300, 1000).millis
    // We can assign same stating time to help forcing and visuqlize the deadlock
    context.system.scheduler.scheduleOnce(500.millis, self, "eat")
    }


  def tryToEat(): Unit = {
    if (!hasFirst) firstFork ! PickUp
    else if (!hasSecond) secondFork ! PickUp
  }

  def receive: Receive = {
    case "eat" =>
      tryToEat()

    case PickedUp =>
      if (!hasFirst) {
        hasFirst = true
        secondFork ! PickUp
      } else if (!hasSecond) {
        hasSecond = true
        eat()
      }

    case Busy =>
        val retryDelay = Random.between(500, 800).millis
        println(s"$name found a fork busy. Retrying in ${retryDelay.toMillis} ms...")
        context.system.scheduler.scheduleOnce(retryDelay, self, "eat")

    case "done" =>
      firstFork ! PutDown

      secondFork ! PutDown
      hasFirst = false
      hasSecond = false
      think()
  }

  def eat(): Unit = {

    meals += 1
    val delay = Random.between(300, 500).millis
    println(s"$name is eating. Total meals: $meals. Eating for ${delay.toMillis} ms...")
    context.system.scheduler.scheduleOnce(delay, self, "done")
    }

}
