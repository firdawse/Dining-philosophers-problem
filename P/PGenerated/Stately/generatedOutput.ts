import { createMachine, assign } from 'xstate';
interface Context {retries: number;}
const Fork = createMachine<Context>({
        id: "Fork",
        initial: "Free", 
        states: {
            Free: {
                on: {
                    ePickUp : { target: [
                        "Held",
                        ]
                    },
                }
            },
            Held: {
                on: {
                    ePutDown : { target: [
                        "Free",
                        ]
                    },
                    ePickUp : { target: [
                        ]
                    },
                }
            }
        }
});
const Philosopher = createMachine<Context>({
        id: "Philosopher",
        initial: "Init", 
        states: {
            Init: {
                always: [
                { target: [
                    "TryRightFirst",
                    "TryLeftFirst",
                    ]
                }
                ],
            },
            TryLeftFirst: {
                on: {
                    ForkGranted : { target: [
                        "TryRight",
                        ]
                    },
                    ForkBusy : { target: [
                        ]
                    },
                }
            },
            TryRightFirst: {
                on: {
                    ForkGranted : { target: [
                        "TryLeft",
                        ]
                    },
                    ForkBusy : { target: [
                        ]
                    },
                }
            },
            TryLeft: {
                on: {
                    ForkGranted : { target: [
                        "Eating",
                        ]
                    },
                    ForkBusy : { target: [
                        ]
                    },
                }
            },
            TryRight: {
                on: {
                    ForkGranted : { target: [
                        "Eating",
                        ]
                    },
                    ForkBusy : { target: [
                        ]
                    },
                }
            },
            Eating: {
                on: {
                    ForkReleased : { target: [
                        "TryRightFirst",
                        "TryLeftFirst",
                        ]
                    },
                }
            }
        }
});
const TestV1 = createMachine<Context>({
        id: "TestV1",
        initial: "Init", 
        states: {
            Init: {
            }
        }
});
const TestV2 = createMachine<Context>({
        id: "TestV2",
        initial: "Init", 
        states: {
            Init: {
            }
        }
});
