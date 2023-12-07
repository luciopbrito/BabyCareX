import { Baba } from "./baba.interface";
import { Base } from "./base.interface";
import { Family } from "./family.interface";

export type ScheduleBase = {
  familyId: number,
  family: Family,
  babaId: number,
  baba: Baba,
  timesAWeek: number,
}

export interface Schedule extends Base, ScheduleBase {}
