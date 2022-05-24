const { dest, parallel, series, src, watch } = require("gulp");
const babel = require("@babel/core");
const babelTemplate = require("@babel/template").default;
const babelTypes = require("@babel/types");
const browserSync = require("browser-sync").create();
const codeFrameColumns = require("@babel/code-frame").codeFrameColumns;
const composer = require("gulp-uglify/composer");
const concat = require("gulp-concat");
const cssmin = require("gulp-cssmin");
const path = require("path");
const rename = require("gulp-rename");
const sass = require("gulp-dart-sass");
const through2 = require("through2");
const uglify0 = require("uglify-es");
const uglify1 = composer(uglify0, console);
const vueCompiler = require("vue-template-compiler");

var output = "wwwroot/assets/";

var css = [
	"src/assets/scss/cpca-nexa.scss",
	"src/assets/scss/cpca-vendors.scss",
	"src/assets/scss/cpca-blocks.scss"
];

var copy = [
	{
		source: "src/assets/img/**/*.*",
		dest: "img"
	},
	{
		source: "node_modules/fancybox/dist/img/*.*",
		dest: "img"
	},
	{
		source: "node_modules/@fortawesome/fontawesome-pro/webfonts/*.*",
		dest: "webfonts"
	},
	{
		source: "node_modules/simple-line-icons/fonts/*.*",
		dest: "webfonts"
	},
	{
		source: "src/assets/vendors/jquery-selectBox/jquery.selectBox-arrow.gif",
		dest: "css"
	},
	{
		source: "node_modules/slick-carousel/slick/*.gif",
		dest: "css"
	},
	{
		source: "node_modules/slick-carousel/slick/fonts/*.*",
		dest: "css/fonts"
	}	
];

var js = [
	{
		name: "cpca-vendors.js",
		items: [
			"node_modules/jquery/dist/jquery.js",
			"node_modules/popper.js/dist/umd/popper.js",
			"node_modules/bootstrap/dist/js/bootstrap.js",
			"node_modules/moment/min/moment.min.js",
			"node_modules/daterangepicker/daterangepicker.js",
			"node_modules/fancybox/dist/js/jquery.fancybox.js",
			"node_modules/imagesloaded/imagesloaded.pkgd.js",
			"node_modules/isotope-layout/dist/isotope.pkgd.js",
			"node_modules/jquery-appear-original/index.js",
			"node_modules/jquery-circle-progress/dist/circle-progress.js",
			"node_modules/slick-carousel/slick/slick.js",
			"src/assets/vendors/jquery-selectBox/jquery.selectBox.js"
		]
	},
	{
		name: "cpca-nexa.js",
		items: [
			"src/assets/js/nexa.js"
		]
	}
];

var vue = [
	{
		name: "cpca-blocks.js",
		items: [
			"src/assets/blocks/accordion-panel-block.vue",
			"src/assets/blocks/card-block.vue",
			"src/assets/blocks/heading-block.vue",
			"src/assets/blocks/window-block.vue"
		]
	}
];

// Compile & minimize less files
function doCSS() {
	return src(css)
		.pipe(sass().on("error", sass.logError))
		.pipe(dest(output + "css"))
		.pipe(browserSync.stream())
		.pipe(cssmin())
		.pipe(rename({
			suffix: ".min"
		}))
		.pipe(dest(output + "css"));
};

// Compile & minimize js files
function doJS(done) {
	for (var n = 0; n < js.length; n++) {
		src(js[n].items, { base: "." })
			.pipe(concat(output + "js/" + js[n].name))
			.pipe(dest("."))
			.pipe(uglify1().on("error", function (e) {
				console.log(e);
			}))
			.pipe(rename({
				suffix: ".min"
			}))
			.pipe(dest("."));
	}
	done();
};

// Compile & minimize vue files
function doVue(done) {
	for (var n = 0; n < vue.length; n++) {
		src(vue[n].items, { base: "." })
			.pipe(vueCompile())
			.pipe(concat(output + "js/" + vue[n].name))
			.pipe(dest("."))
			.pipe(browserSync.stream())
			.pipe(uglify1().on("error", function (e) {
				console.log(e);
			}))
			.pipe(rename({
				suffix: ".min"
			}))
			.pipe(dest("."));
	}
	done();
};

// Copy files
function doCopy(done) {
	for (var n = 0; n < copy.length; n++) {
		src(copy[n].source)
			.pipe(dest(output + copy[n].dest));
	}
	done();
}

function doWatch() {
	watch("src/assets/**/*.scss", doCSS);
	watch("src/assets/js/*.js", doJS);
	watch("src/assets/blocks/*.vue", doVue);

	browserSync.init({
		server: {
			baseDir: "./wwwroot",
			index: "/nexa.html"
		},
		watch: true,
	});
}

function vueCompile() {
	return through2.obj(function (file, _, callback) {
		var relativeFile = path.relative(file.cwd, file.path);
		var ext = path.extname(file.path);
		if (ext === ".vue") {
			var getComponent;
			getComponent = function (ast, sourceCode) {
				const ta = ast.program.body[0]
				if (!babelTypes.isExportDefaultDeclaration(ta)) {
					var msg = 'Top level declaration in file ' + relativeFile + ' must be "export default {" \n' + codeFrameColumns(sourceCode, { start: ta.loc.start }, { highlightCode: true });
					throw msg;
				}
				return ta.declaration;
			}

			var compile;
			compile = function (componentName, content) {
				var component = vueCompiler.parseComponent(content, []);
				if (component.styles.length > 0) {
					component.styles.forEach(s => {
						const linesToStyle = content.substr(0, s.start).split(/\r?\n/).length;
						var msg = "WARNING: <style> tag in " + relativeFile + " is ignored\n" + codeFrameColumns(content, { start: { line: linesToStyle } }, { highlightCode: true });
						console.warn(msg);
					});
				}

				var ast = babel.parse(component.script.content, {
					parserOpts: {
						sourceFilename: file.path
					}
				});

				var vueComponent = getComponent(ast, component.script.content);
				vueComponent.properties.push(babelTypes.objectProperty(babelTypes.identifier("template"), babelTypes.stringLiteral(component.template.content)))

				var wrapInComponent = babelTemplate("Vue.component(NAME, COMPONENT);");
				var componentAst = wrapInComponent({
					NAME: babelTypes.stringLiteral(componentName),
					COMPONENT: vueComponent
				})

				ast.program.body = [componentAst]

				babel.transformFromAst(ast, null, null, function (err, result) {
					if (err) {
						callback(err, null)
					}
					else {
						file.contents = Buffer.from(result.code);
						callback(null, file)
					}
				});
			}
			var componentName = path.basename(file.path, ext);
			if (file.isBuffer()) {
				compile(componentName, file.contents.toString());
			}
			else if (file.isStream()) {
				var chunks = [];
				file.contents.on("data", function (chunk) {
					chunks.push(chunk);
				});
				file.contents.on("end", function () {
					compile(componentName, Buffer.concat(chunks).toString());
				});
			}
		} else {
			callback(null, file);
		}
	});
}

exports.doCSS = doCSS;
exports.doJS = doJS;
exports.doVue = doVue;
exports.doFonts = doCopy;
exports.default = series(parallel(doCSS, doJS, doVue, doCopy), doWatch);
