export type ContentType =
  | 'vocabulary'
  | 'grammarRule'
  | 'sentence'
  | 'exercise'
  | 'interviewQuestion'
  | 'conversationScenario';

export interface ContentModel {
  id: string;
  topicId: string;
  title: string;
  text: string;
  contentType: ContentType;
  difficulty: 'beginner' | 'elementary' | 'intermediate' | 'advanced' | 'proficiency';
}
