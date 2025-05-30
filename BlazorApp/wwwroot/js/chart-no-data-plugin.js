window.registerNoDataPlugin = function () {
    Chart.plugins.register({
        id: 'noData',
        beforeDraw: function (chart) {
            if (chart.data.datasets.length === 0 || chart.data.datasets[0].data.length === 0) {
                const ctx = chart.ctx;
                const width = chart.width;
                const height = chart.height;
                chart.clear();
                ctx.save();
                ctx.textAlign = 'center';
                ctx.textBaseline = 'middle';
                ctx.font = '16px sans-serif';
                ctx.fillText('Нет данных', width / 2, height / 2);
                ctx.restore();
            }
        }
    });
};