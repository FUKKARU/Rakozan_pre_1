using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace RandomMaze
{
    public class StageMaker : MonoBehaviour
    {
        // �ԍ��ƃv���n�u���i�[(25��)
        Dictionary<int, GameObject> dic = new();

        Transform roomTr;

        // 1,2,3,4,5,
        // 6,7,8,9,10,
        // 11,12,13,14,15,
        // 16,17,18,19,20,
        // 21,22,23,24,25
        // �Ɣԍ���U��B�����̊Ԋu��19

        void Start()
        {
            Destroy(GameObject.FindGameObjectWithTag("Room"));
            roomTr = Instantiate(PrefabParamsSO.Entity.Room).transform;

            DecideCorners();
            DecideSides();
            DecideCenters();

            MakeStartAndGoal();
            MakeCorners();
            MakeSides();
            MakeCenters();
        }

        void DecideCorners()
        {
            // �����̔ԍ��������_���ɕ��ёւ�
            List<int> numbers = new() { 1, 5, 21, 25 };
            numbers = numbers.OrderBy(a => Guid.NewGuid()).ToList();

            // �v���n�u�Ɣԍ����A�����Ɋi�[
            for (int i = 0; i < numbers.Count; i++)
            {
                dic.Add(numbers[i], PrefabParamsSO.Entity.CornerRooms[i]);
            }
        }

        void DecideSides()
        {
            // �����̔ԍ��������_���ɕ��ёւ�
            List<int> numbers = new() { 2, 4, 6, 10, 11, 15, 16, 20, 22, 24 };
            numbers = numbers.OrderBy(a => Guid.NewGuid()).ToList();

            // �v���n�u�Ɣԍ����A�����Ɋi�[
            for (int i = 0; i < numbers.Count; i++)
            {
                dic.Add(numbers[i], PrefabParamsSO.Entity.SideRooms[i]);
            }
        }

        void DecideCenters()
        {
            // �����̔ԍ��������_���ɕ��ёւ�
            List<int> numbers = new() { 3, 7, 8, 9, 12, 13, 14, 17, 18, 19, 23 };
            numbers = numbers.OrderBy(a => Guid.NewGuid()).ToList();

            // �v���n�u�Ɣԍ����A�����Ɋi�[
            for (int i = 0; i < numbers.Count; i++)
            {
                dic.Add(numbers[i], PrefabParamsSO.Entity.CenterRooms[i]);
            }
        }

        void MakeStartAndGoal()
        {
            // �X�^�[�g
            Instantiate(PrefabParamsSO.Entity.StartAndGoal[0], new(0f, 0f, 0f), Quaternion.identity, roomTr);

            // �S�[��
            Instantiate(PrefabParamsSO.Entity.StartAndGoal[1], new(0f, 0f, 19f * 6), Quaternion.identity, roomTr);
        }

        void MakeCorners()
        {
            Instantiate(dic[1], new(-19f * 2, 0f, 19f * 5), Quaternion.AngleAxis(90, Vector3.up), roomTr);
            Instantiate(dic[5], new(19f * 2, 0f, 19f * 5), Quaternion.AngleAxis(180, Vector3.up), roomTr);
            Instantiate(dic[21], new(-19f * 2, 0f, 19f), Quaternion.identity, roomTr);
            Instantiate(dic[25], new(19f * 2, 0f, 19f), Quaternion.AngleAxis(270, Vector3.up), roomTr);
        }

        void MakeSides()
        {
            Instantiate(dic[2], new(-19f, 0f, 19f * 5), Quaternion.AngleAxis(180, Vector3.up), roomTr);
            Instantiate(dic[4], new(19f, 0f, 19f * 5), Quaternion.AngleAxis(180, Vector3.up), roomTr);
            Instantiate(dic[6], new(-19f * 2, 0f, 19f * 4), Quaternion.AngleAxis(90, Vector3.up), roomTr);
            Instantiate(dic[10], new(19f * 2, 0f, 19f * 4), Quaternion.AngleAxis(270, Vector3.up), roomTr);
            Instantiate(dic[11], new(-19f * 2, 0f, 19f * 3), Quaternion.AngleAxis(90, Vector3.up), roomTr);
            Instantiate(dic[15], new(19f * 2, 0f, 19f * 3), Quaternion.AngleAxis(270, Vector3.up), roomTr);
            Instantiate(dic[16], new(-19f * 2, 0f, 19f * 2), Quaternion.AngleAxis(90, Vector3.up), roomTr);
            Instantiate(dic[20], new(19f * 2, 0f, 19f * 2), Quaternion.AngleAxis(270, Vector3.up), roomTr);
            Instantiate(dic[22], new(-19f, 0f, 19f * 1), Quaternion.identity, roomTr);
            Instantiate(dic[24], new(19f, 0f, 19f * 1), Quaternion.identity, roomTr);
        }

        void MakeCenters()
        {
            Instantiate(dic[3], new(0f, 0f, 19f * 5), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
            Instantiate(dic[7], new(-19f, 0f, 19f * 4), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
            Instantiate(dic[8], new(0f, 0f, 19f * 4), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
            Instantiate(dic[9], new(19f, 0f, 19f * 4), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
            Instantiate(dic[12], new(-19f, 0f, 19f * 3), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
            Instantiate(dic[13], new(0f, 0f, 19f * 3), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
            Instantiate(dic[14], new(19f, 0f, 19f * 3), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
            Instantiate(dic[17], new(-19f, 0f, 19f * 2), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
            Instantiate(dic[18], new(0f, 0f, 19f * 2), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
            Instantiate(dic[19], new(19f, 0f, 19f * 2), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
            Instantiate(dic[23], new(0f, 0f, 19f), Quaternion.AngleAxis(new List<int>() { 0, 90, 180, 270 }.OrderBy(a => Guid.NewGuid()).ToList()[0], Vector3.up), roomTr);
        }
    }
}
