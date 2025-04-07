import { Stock } from "./Stock";

export interface Order {
    orderId: number;
    orderStateId?: number;
    state?: string;
    prods?: Stock[];
  }