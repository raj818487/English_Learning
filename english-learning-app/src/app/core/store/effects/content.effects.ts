import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ContentService } from '../../services/content.service';
import { ContentActions } from '../actions/content.actions';
import { catchError, map, of, switchMap } from 'rxjs';

@Injectable()
export class ContentEffects {
  loadContent$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ContentActions.loadContent),
      switchMap(() =>
        this.contentService.search('').pipe(
          map(response => ContentActions.loadContentSuccess({ content: response.data })),
          catchError(() => of(ContentActions.loadContentFailure({ error: 'Failed to load content' })))
        )
      )
    )
  );

  constructor(private readonly actions$: Actions, private readonly contentService: ContentService) {}
}
