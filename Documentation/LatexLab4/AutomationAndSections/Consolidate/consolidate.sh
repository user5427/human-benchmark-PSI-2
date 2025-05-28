#!/bin/bash

# Interactive Code File Consolidator
# Usage: ./consolidate.sh <directory_path> [options]

show_help() {
    cat << EOF
Interactive Code File Consolidator

Usage: $0 <directory_path> [options]

Options:
    -h, --help          Show this help message
    -a, --all           Consolidate all supported file types
    -c, --config        Consolidate only configuration files
    -e, --ext EXT       Consolidate specific extension (e.g. -e cs -e tsx)
    -i, --interactive   Interactive mode (default)
    -l, --list          List all files that would be processed (dry run)

Examples:
    $0 ./my-backend                    # Interactive mode
    $0 ./my-backend -e cs              # Only .cs files
    $0 ./my-backend -e cs -e tsx       # Only .cs and .tsx files
    $0 ./my-backend -a                 # All supported file types
    $0 ./my-backend -c                 # Only config files
    $0 ./my-backend -l -e cs           # List .cs files (dry run)

Supported Extensions:
    cs, tsx, ts, js, jsx, vue, py, java, go, php, rb, cpp, c, h, sql, html, css, scss, sass
EOF
}

# Default values
TARGET_DIR=""
INTERACTIVE_MODE=true
DRY_RUN=false
CONSOLIDATE_CONFIG=false
CONSOLIDATE_ALL=false
SELECTED_EXTENSIONS=()

# All supported extensions
ALL_EXTENSIONS=("cs" "tsx" "ts" "js" "jsx" "vue" "py" "java" "go" "php" "rb" "cpp" "c" "h" "sql" "html" "css" "scss" "sass")

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        -h|--help)
            show_help
            exit 0
            ;;
        -a|--all)
            CONSOLIDATE_ALL=true
            INTERACTIVE_MODE=false
            shift
            ;;
        -c|--config)
            CONSOLIDATE_CONFIG=true
            INTERACTIVE_MODE=false
            shift
            ;;
        -e|--ext)
            SELECTED_EXTENSIONS+=("$2")
            INTERACTIVE_MODE=false
            shift 2
            ;;
        -i|--interactive)
            INTERACTIVE_MODE=true
            shift
            ;;
        -l|--list)
            DRY_RUN=true
            shift
            ;;
        -*)
            echo "Unknown option $1"
            show_help
            exit 1
            ;;
        *)
            if [ -z "$TARGET_DIR" ]; then
                TARGET_DIR="$1"
            else
                echo "Multiple directories not supported"
                exit 1
            fi
            shift
            ;;
    esac
done

if [ -z "$TARGET_DIR" ]; then
    echo "Error: Directory path required"
    show_help
    exit 1
fi

if [ ! -d "$TARGET_DIR" ]; then
    echo "Error: Directory '$TARGET_DIR' does not exist"
    exit 1
fi

echo "Analyzing directory: $TARGET_DIR"
echo ""

# Function to count files by extension
count_files() {
    local extension="$1"
    find "$TARGET_DIR" -name "*.$extension" -type f | wc -l
}

# Function to count config files
count_config_files() {
    local count=0
    local config_patterns=(
        "package.json" "*.config.js" "*.config.ts" "appsettings*.json" 
        "web.config" "*.csproj" "*.sln" "tsconfig.json" "tailwind.config.*"
        "next.config.*" "vite.config.*" "webpack.config.*" ".env*" 
        "Dockerfile" "docker-compose*.yml" "*.toml" "*.ini" "*.yaml" "*.yml"
    )
    
    for pattern in "${config_patterns[@]}"; do
        local found=$(find "$TARGET_DIR" -name "$pattern" -type f 2>/dev/null | wc -l)
        count=$((count + found))
    done
    echo $count
}

# Interactive mode
if [ "$INTERACTIVE_MODE" = true ]; then
    echo "📊 File Analysis:"
    echo "=================="
    
    # Show available file types
    echo "Available file types:"
    for ext in "${ALL_EXTENSIONS[@]}"; do
        count=$(count_files "$ext")
        if [ $count -gt 0 ]; then
            printf "  %-6s : %d files\n" "$ext" "$count"
        fi
    done
    
    config_count=$(count_config_files)
    if [ $config_count -gt 0 ]; then
        printf "  %-6s : %d files\n" "config" "$config_count"
    fi
    
    echo ""
    echo "What would you like to consolidate?"
    echo "1) All file types"
    echo "2) Specific file extensions"
    echo "3) Configuration files only"
    echo "4) Custom selection"
    echo ""
    read -p "Choose option (1-4): " choice
    
    case $choice in
        1)
            CONSOLIDATE_ALL=true
            ;;
        2)
            echo ""
            echo "Available extensions with files:"
            for ext in "${ALL_EXTENSIONS[@]}"; do
                count=$(count_files "$ext")
                if [ $count -gt 0 ]; then
                    printf "  %s (%d files)\n" "$ext" "$count"
                fi
            done
            echo ""
            read -p "Enter extensions separated by spaces (e.g., cs tsx ts): " extensions_input
            IFS=' ' read -ra SELECTED_EXTENSIONS <<< "$extensions_input"
            ;;
        3)
            CONSOLIDATE_CONFIG=true
            ;;
        4)
            echo ""
            echo "Select what to consolidate:"
            
            # File extensions
            echo "File extensions:"
            for ext in "${ALL_EXTENSIONS[@]}"; do
                count=$(count_files "$ext")
                if [ $count -gt 0 ]; then
                    read -p "  Include .$ext files ($count found)? [y/N]: " include
                    if [[ $include =~ ^[Yy]$ ]]; then
                        SELECTED_EXTENSIONS+=("$ext")
                    fi
                fi
            done
            
            # Config files
            if [ $config_count -gt 0 ]; then
                read -p "  Include configuration files ($config_count found)? [y/N]: " include_config
                if [[ $include_config =~ ^[Yy]$ ]]; then
                    CONSOLIDATE_CONFIG=true
                fi
            fi
            ;;
        *)
            echo "Invalid choice"
            exit 1
            ;;
    esac
fi

# Set extensions based on flags
if [ "$CONSOLIDATE_ALL" = true ]; then
    SELECTED_EXTENSIONS=("${ALL_EXTENSIONS[@]}")
    CONSOLIDATE_CONFIG=true
fi

# Dry run - just list files
if [ "$DRY_RUN" = true ]; then
    echo "🔍 Files that would be processed:"
    echo "================================="
    
    if [ ${#SELECTED_EXTENSIONS[@]} -gt 0 ]; then
        for ext in "${SELECTED_EXTENSIONS[@]}"; do
            echo ""
            echo ".$ext files:"
            find "$TARGET_DIR" -name "*.$ext" -type f | sort
        done
    fi
    
    if [ "$CONSOLIDATE_CONFIG" = true ]; then
        echo ""
        echo "Configuration files:"
        config_patterns=("package.json" "*.config.js" "*.config.ts" "appsettings*.json" "web.config" "*.csproj" "*.sln" "tsconfig.json" "tailwind.config.*" "next.config.*" "vite.config.*" "webpack.config.*" ".env*" "Dockerfile" "docker-compose*.yml" "*.toml" "*.ini" "*.yaml" "*.yml")
        for pattern in "${config_patterns[@]}"; do
            find "$TARGET_DIR" -name "$pattern" -type f 2>/dev/null | sort
        done
    fi
    exit 0
fi

# Create output directory
OUTPUT_DIR="consolidated_code"
mkdir -p "$OUTPUT_DIR"

echo ""
echo "🔄 Processing files..."
echo "======================"

# Function to consolidate files by extension
consolidate_files() {
    local extension="$1"
    local output_file="all_${extension}_files.${extension}"
    local file_count=0
    
    # Clear output file
    > "$OUTPUT_DIR/$output_file"
    
    echo "Processing *.$extension files..."
    
    # Find all files with the extension and process them
    while IFS= read -r -d '' file; do
        if [ -f "$file" ]; then
            echo "" >> "$OUTPUT_DIR/$output_file"
            echo "// ==================== FILE: $file ====================" >> "$OUTPUT_DIR/$output_file"
            echo "" >> "$OUTPUT_DIR/$output_file"
            cat "$file" >> "$OUTPUT_DIR/$output_file"
            echo "" >> "$OUTPUT_DIR/$output_file"
            ((file_count++))
        fi
    done < <(find "$TARGET_DIR" -name "*.$extension" -type f -print0)
    
    if [ $file_count -gt 0 ]; then
        echo "  → Created $OUTPUT_DIR/$output_file with $file_count files"
    else
        echo "  → No *.$extension files found"
        rm -f "$OUTPUT_DIR/$output_file"
    fi
}

# Function to consolidate config files
consolidate_config_files() {
    local output_file="config_files.txt"
    local file_count=0
    
    > "$OUTPUT_DIR/$output_file"
    
    echo "Processing configuration files..."
    
    config_patterns=("package.json" "*.config.js" "*.config.ts" "appsettings*.json" "web.config" "*.csproj" "*.sln" "tsconfig.json" "tailwind.config.*" "next.config.*" "vite.config.*" "webpack.config.*" ".env*" "Dockerfile" "docker-compose*.yml" "*.toml" "*.ini" "*.yaml" "*.yml")
    
    for pattern in "${config_patterns[@]}"; do
        while IFS= read -r -d '' file; do
            if [ -f "$file" ]; then
                echo "" >> "$OUTPUT_DIR/$output_file"
                echo "// ==================== CONFIG FILE: $file ====================" >> "$OUTPUT_DIR/$output_file"
                echo "" >> "$OUTPUT_DIR/$output_file"
                cat "$file" >> "$OUTPUT_DIR/$output_file"
                echo "" >> "$OUTPUT_DIR/$output_file"
                ((file_count++))
            fi
        done < <(find "$TARGET_DIR" -name "$pattern" -type f -print0 2>/dev/null)
    done
    
    if [ $file_count -gt 0 ]; then
        echo "  → Created $OUTPUT_DIR/$output_file with $file_count files"
    else
        echo "  → No configuration files found"
        rm -f "$OUTPUT_DIR/$output_file"
    fi
}

# Process selected extensions
for ext in "${SELECTED_EXTENSIONS[@]}"; do
    consolidate_files "$ext"
done

# Process config files if requested
if [ "$CONSOLIDATE_CONFIG" = true ]; then
    consolidate_config_files
fi

# Create summary
echo ""
echo "📝 Creating summary..."
cat > "$OUTPUT_DIR/consolidation_summary.txt" << EOF
File Consolidation Summary
=========================
Source Directory: $TARGET_DIR
Generated: $(date)
Command: $0 $*

Consolidated Files:
EOF

for file in "$OUTPUT_DIR"/*; do
    if [ -f "$file" ] && [ "$(basename "$file")" != "consolidation_summary.txt" ]; then
        filename=$(basename "$file")
        filesize=$(wc -l < "$file")
        echo "  - $filename ($filesize lines)" >> "$OUTPUT_DIR/consolidation_summary.txt"
    fi
done

echo ""
echo "✅ Consolidation complete!"
echo "📁 Output directory: $OUTPUT_DIR/"
echo ""
echo "📋 Files created:"
ls -la "$OUTPUT_DIR/"
echo ""
echo "💡 Usage tip: Feed these consolidated files to an LLM for comprehensive analysis"