import { createContext } from "react";

import { UiStore } from "./uiStore";
import CounterStore from "./counterStore";

interface Store {
  counterStore: CounterStore;
  uiStore: UiStore;
}

export const store: Store = {
  counterStore: new CounterStore(),
  uiStore: new UiStore(),
};

export const StoreContext = createContext(store);
