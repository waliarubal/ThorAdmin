namespace ThorAdmin.Api.Base;

public sealed class ApiResponse<TData>
{
    readonly TData _data;
    readonly string _error;

    public ApiResponse(TData data, string error = "")
    {
        _data = data;
        _error = error;
    }

    #region properties

    public TData Data => _data;

    public string Error => _error;

    #endregion
}
