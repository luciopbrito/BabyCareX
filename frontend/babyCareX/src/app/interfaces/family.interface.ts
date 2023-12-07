import { Base } from "./base.interface"
import { Child } from "./child.interface"
import { Schedule } from "./schedule.interface"

export interface FamilyBase {
  familyName: string,
  fatherName: string,
  motherName: string | null,
  phoneNumber: number,
  email: string,
  password: string,
  children: Child[],
  schedules: Schedule[]
}

export interface Family extends Base, FamilyBase {}
