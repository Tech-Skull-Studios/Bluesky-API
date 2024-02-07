/*
 BLUESKY API
 DATA INTERFACE
 IRESPONSE DATA
 v1.0
 LAST EDITED: SUNDAY FEBRUARY 19, 2023
 COPYRIGHT © TECH SKULL STUDIOS
*/

namespace Bluesky.Data
{
    /// <summary>
    /// Defines a response that returns data.
    /// </summary>
    /// <typeparam name="T">Type of data returned in the response.</typeparam>
    public interface IResponseData<T> : IResponse
    {
        T data { get; }
    }
}