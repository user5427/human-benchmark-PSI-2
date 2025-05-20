#!/bin/bash

echo "🧹 Cleaning up split files..."

# Find and delete only *_part_*.md files
find ./Sections -type f -name "*_part_*.md" -exec rm -v {} \;

echo "✅ Cleanup complete. Original and FULL files are untouched."
