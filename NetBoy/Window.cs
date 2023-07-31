using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace NetBoy
{
    public class Window
    {
        private bool quit = false;
        public void Init(string title, int w, int h)
        {
            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
            var window = IntPtr.Zero;

            window = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, w, h, SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            
            SDL.SDL_Event eventItem;

            while(!quit)
            {
                while(SDL.SDL_PollEvent(out eventItem) != 0)
                {
                    switch (eventItem.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            quit = true;
                            Console.WriteLine("Window Closed.");
                            break;


                    }

                }


            }


            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();



        }



    }
}
