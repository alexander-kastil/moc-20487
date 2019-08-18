
var gulp = require('gulp');
var concat = require('gulp-concat');
var cssmin = require('gulp-cssmin');
var uglify = require('gulp-uglify');
var cache = require('gulp-cached'); 

var paths = {
    webroot: "./wwwroot/",
    jsSource: "./Scripts/*",
    jsDest: "./wwwroot/js/"
}

gulp.task('min:js', function () {
    return gulp.src([paths.jsSource])
        .pipe(concat(paths.jsDest + "min/site.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task('copy:js', function () {
    return gulp.src([paths.jsSource])
        .pipe(gulp.dest(paths.jsDest));
});

gulp.task("both:tasks", ["min:js", "copy:js"]);

gulp.task('copy:node', function () {
try {
    gulp.src('node_modules/**')
        .pipe(cache('node_modules'))
        .pipe(gulp.dest('wwwroot/node_modules'));
    }
    catch (e) 
    {
        return -1;
    }
    return 0;
});