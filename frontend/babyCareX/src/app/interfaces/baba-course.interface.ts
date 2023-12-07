import { Baba } from "./baba.interface";
import { Base } from "./base.interface";

export interface BabaCourseBase {
  name: string,
  startPeriod: Date,
  endPeriod: null,
  isCompleted: false,
  babaId: number,
  baba: Baba,
}

export interface BabaCourse extends Base, BabaCourseBase {}
