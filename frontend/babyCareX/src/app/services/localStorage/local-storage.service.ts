import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  constructor() { }

  public getItem(keyItem: string): object | null {
    const eText = localStorage.getItem(keyItem);

    if (eText) {
      const bytes = CryptoJS.AES.decrypt(eText, keyItem)
      const b = bytes.toString(CryptoJS.enc.Utf8)
      const obj = JSON.parse(b)
      return obj as object;
    }
    else {
      return null;
    }

  };

  public setItem(key: string, value: any): void {
    localStorage.setItem(
      key,
      CryptoJS.AES.encrypt(JSON.stringify(value), key).toString(),
    );
  };

  public removeItem(keyItem: string): void {
    localStorage.removeItem(keyItem);
  };
}
