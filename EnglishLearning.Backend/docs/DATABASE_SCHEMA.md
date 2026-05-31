# Database Schema

Main entity sets configured in `ApplicationDbContext`:
- Topics
- Contents (TPH with Vocabularies, GrammarRules, Sentences, Exercises, InterviewQuestions, ConversationScenarios)
- UserProgresses
- ContentHashes

Deduplication registry uses `ContentHashes.Hash` as a unique indexed field.
