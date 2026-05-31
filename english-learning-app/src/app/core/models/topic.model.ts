export interface TopicModel {
  id: string;
  name: string;
  category: 'general' | 'business' | 'interview' | 'grammar' | 'vocabulary';
  difficulty: 'beginner' | 'elementary' | 'intermediate' | 'advanced' | 'proficiency';
}
