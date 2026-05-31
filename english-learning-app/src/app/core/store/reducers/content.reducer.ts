import { createReducer, on } from '@ngrx/store';
import { ContentActions } from '../actions/content.actions';
import { ContentModel } from '../../models/content.model';

export interface ContentState {
  items: ContentModel[];
  loading: boolean;
  error: string | null;
}

export const initialState: ContentState = {
  items: [],
  loading: false,
  error: null
};

export const contentReducer = createReducer(
  initialState,
  on(ContentActions.loadContent, state => ({ ...state, loading: true, error: null })),
  on(ContentActions.loadContentSuccess, (state, { content }) => ({ ...state, loading: false, items: content })),
  on(ContentActions.loadContentFailure, (state, { error }) => ({ ...state, loading: false, error }))
);
