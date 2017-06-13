using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    public static partial class SDL
    {
        /**
 *  \brief  The structure that defines a point
 *
 *  \sa SDL_EnclosePoints
 *  \sa SDL_PointInRect
 */
        typedef struct SDL_Point
        {
            int x;
            int y;
        }
        SDL_Point;

/**
 *  \brief A rectangle, with the origin at the upper left.
 *
 *  \sa SDL_RectEmpty
 *  \sa SDL_RectEquals
 *  \sa SDL_HasIntersection
 *  \sa SDL_IntersectRect
 *  \sa SDL_UnionRect
 *  \sa SDL_EnclosePoints
 */
typedef struct SDL_Rect
        {
            int x, y;
            int w, h;
        }
        SDL_Rect;

/**
 *  \brief Returns true if point resides inside a rectangle.
 */
SDL_FORCE_INLINE SDL_bool SDL_PointInRect (const SDL_Point* p, const SDL_Rect* r)
{
    return ((p->x >= r->x) && (p->x<(r->x + r->w)) &&
             (p->y >= r->y) && (p->y<(r->y + r->h)) ) ? SDL_TRUE : SDL_FALSE;
}

    /**
     *  \brief Returns true if the rectangle has no area.
     */
    SDL_FORCE_INLINE SDL_bool SDL_RectEmpty (const SDL_Rect* r)
    {
        return ((!r) || (r->w <= 0) || (r->h <= 0)) ? SDL_TRUE : SDL_FALSE;
    }

    /**
     *  \brief Returns true if the two rectangles are equal.
     */
    SDL_FORCE_INLINE SDL_bool SDL_RectEquals (const SDL_Rect* a, const SDL_Rect* b)
    {
        return (a && b && (a->x == b->x) && (a->y == b->y) &&
                (a->w == b->w) && (a->h == b->h)) ? SDL_TRUE : SDL_FALSE;
    }

/**
 *  \brief Determine whether two rectangles intersect.
 *
 *  \return SDL_TRUE if there is an intersection, SDL_FALSE otherwise.
 */
extern DECLSPEC SDL_bool SDLCALL SDL_HasIntersection (const SDL_Rect* A,
                                                     const SDL_Rect* B);

/**
 *  \brief Calculate the intersection of two rectangles.
 *
 *  \return SDL_TRUE if there is an intersection, SDL_FALSE otherwise.
 */
extern DECLSPEC SDL_bool SDLCALL SDL_IntersectRect (const SDL_Rect* A,
                                                   const SDL_Rect* B,
                                                   SDL_Rect* result);

/**
 *  \brief Calculate the union of two rectangles.
 */
extern DECLSPEC void SDLCALL SDL_UnionRect (const SDL_Rect* A,
                                           const SDL_Rect* B,
                                           SDL_Rect* result);

/**
 *  \brief Calculate a minimal rectangle enclosing a set of points
 *
 *  \return SDL_TRUE if any points were within the clipping rect
 */
extern DECLSPEC SDL_bool SDLCALL SDL_EnclosePoints (const SDL_Point* points,
                                                   int count,
                                                   const SDL_Rect* clip,
                                                   SDL_Rect* result);

/**
 *  \brief Calculate the intersection of a rectangle and line segment.
 *
 *  \return SDL_TRUE if there is an intersection, SDL_FALSE otherwise.
 */
extern DECLSPEC SDL_bool SDLCALL SDL_IntersectRectAndLine (const SDL_Rect*
                                                          rect, int* X1,
                                                          int* Y1, int* X2,
                                                          int* Y2);
}
}