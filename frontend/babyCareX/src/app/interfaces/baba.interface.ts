import { BabaCapacity } from "./baba-capacity.interface"
import { BabaCourse } from "./baba-course.interface"
import { BabaProvideService } from "./baba-provider-service.interface"
import { Base } from "./base.interface"
import { Schedule } from "./schedule.interface"

export type BabaBase = {
  name: string,
  email: string,
  phoneNumber: string,
  password: string,
  description: null,
  schedules: Schedule[],
  babaProvideServices: BabaProvideService[],
  babaCourses: BabaCourse[],
  babaCapacities: BabaCapacity[],
}

export interface Baba extends Base, BabaBase {}
