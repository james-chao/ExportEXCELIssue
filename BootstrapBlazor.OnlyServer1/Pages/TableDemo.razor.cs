using BootstrapBlazor.Components;
using BootstrapBlazor.OnlyServer1.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.OnlyServer1.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableDemo : ComponentBase
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        [Inject]
        [NotNull]
        private ITableExport? TableExport { get; set; }

        private readonly ConcurrentDictionary<Foo, IEnumerable<SelectedItem>> _cache = new();

        private IEnumerable<SelectedItem> GetHobbys(Foo item) => _cache.GetOrAdd(item, f => Foo.GenerateHobbys(Localizer));

        /// <summary>
        /// 
        /// </summary>
        private static IEnumerable<int> PageItemsSource => new int[] { 20, 40 };

        private async Task<bool> OnExportAsync(ITableExportDataContext<Foo> context)
        {
            // 自定义导出方法
            // 通过 context 参数可以自己查询数据进行导出操作
            // 本例使用 context 传递来的 Rows/Columns 自定义文件名为 Test.xlsx
            var ret = await TableExport.ExportExcelAsync(context.Rows, context.Columns, "Test.xlsx");

            // 返回 true 时自动弹出提示框
            return ret;
        }
    }
}
