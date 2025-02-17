
//function GetProductWiseSalesGraph(regionid=0) {
//    var dataObject = JSON.stringify({
//        'RegionID': regionid,
//        'BranchID': $('#BranchID').val(),
//        'InputDate': $('#InputDate').val()
//    });
//    $.ajax({
//        url: "/Home/_ProductSalesGraph",
//        type: "Get",
//        data: dataObject,
//        success: function (data) {
//            $("#ProductWiseSaleGraph").empty();
//            $("#ProductWiseSaleGraph").html(data);
//            $("#RegionID").val(regionid);
//        },
//        error: function (er) {
//            alert(er);

//        }
//    });
//}
//function GetBranchbyRegion(regionid) {
//    let html = ``;
//    var dataObject = JSON.stringify({
//        'Values': regionid,
//        'Doctype': "GraphFilterBranch"
//    });
//    $.ajax({
//        url: '/Home/GetRegionWiseBranchListJson',
//        dataType: "json",
//        contentType: 'application/json',
//        type: "Post",
//        data: dataObject,
//        success: function (res) {
//            html = `<option value="0">--Select--</option>`;
//            $.each(res, function (k, v) {
//                html += `<option value="` + v.ID + `">` + v.Name + `</option>`;
//            })
//            $('#BranchID').html(html);
//        }
//    });
//}
//(function ($) {
//	"use strict";

//	/*chart-pie*/
//	var chart = c3.generate({
//		bindto: '#chart_pie', // id of chart wrapper
//		data: {
//			columns: [
//				// each columns data
//				['data1', 60],
//				['data2', 20],
//				['data3', 20],
//				['data4', 10]
//			],
//			type: 'pie', // default type of chart
//			colors: {
//				'data1': '#4454c3',
//				'data2': '#f72d66',
//				'data3': '#c344ff',
//				'data4': '#5ed94c'

//			},
//			names: {
//				// name of each serie
//				'data1': 'A',
//				'data2': 'B',
//				'data3': 'C',
//				'data4': 'D'
//			}
//		},
//		axis: {
//		},
//		legend: {
//			show: true, //hide legend
//		},
//		padding: {
//			bottom: 0,
//			top: 0
//		},
//	});
//})(jQuery);