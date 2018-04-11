//把字符串转换为json
function strToJson(strJson) {
    if(strJson == null || strJson == undefined || strJson == "") {
        return {};
    }
    return eval('(' + strJson + ')');
}