import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { ContentModel } from '../../models/content.model';

export const ContentActions = createActionGroup({
  source: 'Content',
  events: {
    'Load Content': emptyProps(),
    'Load Content Success': props<{ content: ContentModel[] }>(),
    'Load Content Failure': props<{ error: string }>()
  }
});
