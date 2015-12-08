using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFClassLibrary.Common.API.VK
{
    public enum VKScopes
    {
        notify = 1,         // Пользователь разрешил отправлять ему уведомления. 
        friends = 2,        // Доступ к друзьям.
        photos = 4,         // Доступ к фотографиям. 
        audio = 8,          // Доступ к аудиозаписям. 
        video = 16,         // Доступ к видеозаписям. 
        offers = 32,        // Доступ к предложениям (устаревшие методы). 
        questions = 64,     // Доступ к вопросам (устаревшие методы). 
        pages = 128,        // Доступ к wiki-страницам. 
        link = 256,         // Добавление ссылки на приложение в меню слева.
        notes = 2048,       // Доступ заметкам пользователя. 
        messages = 4096,    // (для Standalone-приложений) Доступ к расширенным методам работы с сообщениями. 
        wall = 8192,        // Доступ к обычным и расширенным методам работы со стеной. 
        docs = 131072       // Доступ к документам пользователя.
    }
}
