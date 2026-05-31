import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class LoggerService {
  info(message: string) {
    console.info(message);
  }
}
