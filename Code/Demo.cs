/*
 BLUESKY API
 DEMO CLASS
 DEMO
 v1.0
 LAST EDITED: WEDNESDAY FEBRUARY 7, 2024
 COPYRIGHT © TECH SKULL STUDIOS
*/

using UnityEngine;

namespace Bluesky.Demo
{
    public class Demo : MonoBehaviour
    {
        void Start()
        {
            //BlueskyAPI.Login(new LoginSession("drakebates", "4849hadTTOSBT"));
            //BlueskyAPI.GetPreferences();
            //BlueskyAPI.GetProfile("drakebates.bsky.social");
            //BlueskyAPI.GetProfiles("drakebates.bsky.social"/*, "gthederg.bsky.social"*/);
            //BlueskyAPI.GetSuggested();
            BlueskyAPI.GetFeed("did:plc:z72i7hdynmk6r22z27h6tvur");
        }
    }
}