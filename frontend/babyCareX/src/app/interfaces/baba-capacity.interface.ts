import { Baba } from "./baba.interface";
import { Base } from "./base.interface";

export type BabaCapacityBase = {
  name: string,
  babaId: number,
  baba: Baba,
}

export interface BabaCapacity extends Base, BabaCapacityBase {}
