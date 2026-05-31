# English Learning Platform

This repository contains a full-stack English learning platform scaffold:

- **Backend**: `EnglishLearning.Backend/` (.NET 8, Clean Architecture)
- **Frontend**: `english-learning-app/` (Angular 17 with clean feature-oriented folder layout)

## Backend

### Run
```bash
cd /tmp/workspace/raj818487/English_Learning/EnglishLearning.Backend/src/EnglishLearning.API
dotnet run
```

### Test
```bash
cd /tmp/workspace/raj818487/English_Learning/EnglishLearning.Backend
dotnet test
```

### Implemented core features
- Clean Architecture 4-layer structure
- Content/topic entities and services
- 3-level deduplication (hash, semantic similarity, metadata)
- Standardized API response format
- JWT authentication setup
- Exception handling middleware

## Frontend

### Install & Run
```bash
cd /tmp/workspace/raj818487/English_Learning/english-learning-app
npm install
npm start
```

### Test
```bash
npm test
```

### Frontend scaffold includes
- Core/shared/features folder architecture
- Content NgRx action/reducer/effect/selector baseline
- Content/topic/interview/exercise service stubs
- i18n files (`en`, `es`, `fr`)
- Environment and styles structure
