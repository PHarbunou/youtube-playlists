cls
@echo off
del /F *.html

youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFwz7Z2SXfmCL7Bhigy8VjLm -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFytjQY_DpPYa5jvqESdBjts -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFxUBLnOL7g6x8QaNUZ-Agif -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFzTxhZ3sMN-dxtiJ_cQ659F -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFyKrvSCUpgJC9u1QF-Vr1zk -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFyMiPmw0BXW0Iz4fyrig-9n -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFwvcH0ek6QO8Pa-MRMn7H8Q -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFzRda4pAY7l2YbaOZL_aD0H -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFxBdX7nnxPd3f3B83se-9JS -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFzboYuarK_z9ZsP1vbPNh3T -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFxAS9zZGssM7MwFkvgJuOYV -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFzCLNUBJ6THbV1tQrXjP_me -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFx5ix_ky0nnZXlWymn2bzbW -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFzy5PLwjDioVHCdbVk9uRD5 -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFxKijzUFqcAjgzMEdltxNYI -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFwmEkzbqPURB-s1ujAuXQF3 -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFz7OQ5UTUuZ8EB6a_wKpWUG -body
youtube-playlists.exe https://www.youtube.com/playlist?list=PLPMUUio-TPFwu1_VhoSVR_u92hgLxNwoT -body

cls
@echo Build HTML
type html_header_japan.txt >> index.html
type "GDT-Windows 1.*.html" >> index.html
type "GDT-Windows 2.*.html" >> index.html
type "GDT-Windows 3.*.html" >> index.html
type "GDT-Windows 4.*.html" >> index.html
type "GDT-Windows 5.*.html" >> index.html
type "GDT-Windows 6.*.html" >> index.html
type "GDT-Windows 7.*.html" >> index.html
type "GDT-Windows 8.*.html" >> index.html
type "GDT-Windows 9.*.html" >> index.html
type "GDT-Windows 10.*.html" >> index.html
type "GDT-Windows 11.*.html" >> index.html
type "GDT-Windows 12.*.html" >> index.html
type "GDT-Windows 13.*.html" >> index.html
type "GDT-Windows 14.*.html" >> index.html
type "GDT-Windows 15.*.html" >> index.html
type "GDT-Windows 16.*.html" >> index.html
type "GDT-Windows 17.*.html" >> index.html
type "GDT-Windows 18.*.html" >> index.html
type html_footer.txt >> index.html

cls
@echo Start HTML
cmd /c start index.html

