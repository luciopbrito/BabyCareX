import { Base } from "./base.interface"
import { Family } from "./family.interface"

export type ChildBase = {
  name: string,
  age: number,
  sex: number,
  isSpecialChild: boolean,
  familyId: number,
  family: Family,
}

export interface Child extends Base, ChildBase {}
