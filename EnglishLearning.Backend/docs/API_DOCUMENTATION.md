# API Documentation

## Base URL
`/api`

## Standard response
All endpoints return:

```json
{
  "success": true,
  "message": "Operation completed successfully",
  "data": {},
  "errors": null,
  "timestamp": "2026-05-31T10:30:00Z"
}
```

## Endpoints
- `GET /api/health`
- `POST /api/topics`
- `GET /api/topics`
- `POST /api/content/vocabulary` (JWT protected)
- `GET /api/content/search?query=...`
