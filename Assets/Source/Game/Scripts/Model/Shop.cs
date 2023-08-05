using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryOfGoods.Model
{
    public class Shop
    {
        public void Show(BoxPresenter box)
        {
            box.gameObject.SetActive(true);
            box.transform.LeanMoveZ(box.TargetPosition.z, 0.2f);
        }

        public void Clean(BoxPresenter box)
        {
            box.gameObject.SetActive(false);
            box.transform.LeanMoveZ(box.transform.position.z - 20, 0.1f);
        }
    }
}