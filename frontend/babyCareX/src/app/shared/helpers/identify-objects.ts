import { Baba } from "src/app/interfaces/baba.interface";
import { Family } from "src/app/interfaces/family.interface";

function isFamilyOBject(object: any): object is Family {
  return 'familyName' in object;
}
function isBabaOBject(object: any): object is Baba {
  return 'babaProvideServices' in object;
}
export const identifyObject = {
  isBabaOBject,
  isFamilyOBject
}
