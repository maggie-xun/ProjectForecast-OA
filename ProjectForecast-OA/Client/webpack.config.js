module.exports = {
    entry: './app.js',
    output: {
        path: __dirname,
        filename: '_dist/bundle.js'
    },

    resolve: {
        extensions: [".js", ".ts", ".tsx", ".vue"],
        alias: {
            'vue$': 'vue/dist/vue.common.js'
          }
    },

    module: {
        rules: [
            { test: /\.vue$/, loader: 'vue-loader' },
            { test: /\.js$/, exclude: /node_modules/, loader: "babel-loader" },
            { test: /\.(png|jpg)$/, loader: 'url-loader?limit=1024000&name=./../Client/_dist/res/[name].[ext]?[hash]' },
            { test: /\.css$/, loader: 'style-loader!css-loader' },
            {
                test: /\.(eot|svg|ttf|woff|woff2)(\?\S*)?$/,
                loader: 'file-loader',
                query: {
                    name: './../Client/_dist/res/[name].[ext]?[hash]'
                }
            }
        ],
    },

    externals: {
        'jquery': 'jQuery'
    },

    // devtool: "cheap-module-eval-source-map"
}