import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ContentState } from '../reducers/content.reducer';

const selectContentFeature = createFeatureSelector<ContentState>('content');

export const selectContentItems = createSelector(selectContentFeature, state => state.items);
export const selectContentLoading = createSelector(selectContentFeature, state => state.loading);
