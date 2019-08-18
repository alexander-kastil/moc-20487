var gulp = require("gulp");
var concat = require("gulp-concat");
var uglify = require("gulp-uglify");
var sass = require("gulp-sass");
var babel = require("gulp-babel");
var sourcemaps = require("gulp-sourcemaps");

var paths = {
  webroot: "./wwwroot/",
  scriptSource: "./wwwroot/demos/*.js",
  scriptDest: "./wwwroot/js/",
  demos: "./wwwroot/demos/",
  scss: "./wwwroot/sass/**/*.scss",
  scssDest: "./wwwroot/css/"
};

gulp.task("minjs", () => {
  return gulp
    .src([paths.scriptSource])
    .pipe(concat(paths.scriptDest + "min/site.min.js"))
    .pipe(uglify())
    .pipe(gulp.dest("."));
});

gulp.task("copyjs", () => {
  return gulp.src([paths.scriptSource]).pipe(gulp.dest(paths.scriptDest));
});

gulp.task("compile-sass", () => {
  return gulp
    .src(paths.scss)
    .pipe(sass())
    .pipe(gulp.dest(paths.scssDest));
});

gulp.task("babel2es5", () => {
  return gulp
    .src([paths.demos + "es6*.js"])
    .pipe(sourcemaps.init())
    .pipe(
      babel({
        presets: ["es2015"]
      })
    )
    .pipe(concat("es5bundle.js"))
    .pipe(sourcemaps.write("."))
    .pipe(gulp.dest(paths.demos));
});
