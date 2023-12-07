import { BabaProvideService } from "./baba-provider-service.interface"
import { Base } from "./base.interface"

export type KindNanniesBase = {
  description: string,
  name: string,
  babaProvideServices: BabaProvideService[]
}

export interface KindNannies extends Base, KindNanniesBase {}
