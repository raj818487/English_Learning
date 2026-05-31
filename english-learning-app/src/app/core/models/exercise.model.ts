export interface ExerciseModel {
  id: string;
  topicId: string;
  text: string;
  exerciseType: 'multipleChoice' | 'fillInTheBlanks' | 'essay';
  correctAnswer: string;
}
