/*
 BLUESKY API
 DATA INTERFACE
 IREQUEST
 v1.0
 LAST EDITED: FRIDAY SEPTEMBER 16, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

namespace Bluesky.Data
{
    /// <summary>
    /// Used to define a request.
    /// </summary>
    public interface IRequest
    {
        string Command { get; }
    }
}