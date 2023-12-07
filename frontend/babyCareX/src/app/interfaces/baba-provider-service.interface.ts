import { Baba } from "./baba.interface"
import { Base } from "./base.interface"

export type BabaProvideServicesBase = {
  kindNannyId: number,
  babaId: number,
  baba: Baba,
}

export interface BabaProvideService extends Base, BabaProvideServicesBase {}
