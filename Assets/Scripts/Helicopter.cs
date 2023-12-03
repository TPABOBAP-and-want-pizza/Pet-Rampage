using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Vector3 targetDayPosition; // ѕример координат дл€ дн€
    public Vector3 targetNightPosition; // ѕример координат дл€ ночи
    public Vector3 nightTeleportPosition; //  оординаты дл€ телепортации в начале ночи
    public float speed = 5.0f;

    [SerializeField] private GlobalTimer globalTimer; // —сылка на скрипт GlobalTimer через инспектор

    private bool hasTeleportedAtNight = false; // ‘лаг дл€ отслеживани€ телепортации в начале ночи
    private bool lastDayState = true; // ѕеременна€ дл€ хранени€ последнего состо€ни€ дн€

    void Update()
    {
        if (globalTimer != null)
        {
            bool isDay = globalTimer.IsDayTime;

            // ≈сли последнее состо€ние было днем, а сейчас наступила ночь, то устанавливаем флаг телепортации
            if (lastDayState && !isDay)
            {
                hasTeleportedAtNight = false;
            }

            // —охран€ем текущее состо€ние дл€ следующего кадра
            lastDayState = isDay;

            // ≈сли день, летим к цели дл€ дн€, иначе к цели дл€ ночи
            Vector3 target = isDay ? targetDayPosition : targetNightPosition;

            float step = speed * Time.deltaTime;

            if (!isDay)
            {
                // ≈сли наступает ночь и телепорт еще не был выполнен, телепортировать вертолет
                if (!hasTeleportedAtNight)
                {
                    transform.position = nightTeleportPosition;
                    hasTeleportedAtNight = true; // ”станавливаем флаг телепортации в начале ночи
                }
                else
                {
                    // »наче двигатьс€ к цели дл€ ночи
                    transform.position = Vector3.MoveTowards(transform.position, target, step*3);
                }
            }
            else
            {
                // ƒвижение к цели дл€ дн€
                transform.position = Vector3.MoveTowards(transform.position, target, step*3);
            }
        }
    }
}
