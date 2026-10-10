# -*- coding: utf-8 -*-
import unittest

from gilded_rose import Item, GildedRose


class GildedRoseTest(unittest.TestCase):

    def test_normal_item_quality_decreases(self):
        items = [Item("foo", 10, 20)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(19, items[0].quality)
        self.assertEqual(9, items[0].sell_in)

    def test_normal_item_quality_decreases_twice_after_sell_in(self):
        items = [Item("foo", 0, 20)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(18, items[0].quality)
        self.assertEqual(-1, items[0].sell_in)

    def test_quality_never_becomes_negative(self):
        items = [Item("foo", 5, 0)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(0, items[0].quality)

    def test_aged_brie_quality_increases(self):
        items = [Item("Aged Brie", 5, 10)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(11, items[0].quality)

    def test_quality_never_exceeds_50(self):
        items = [Item("Aged Brie", 5, 50)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(50, items[0].quality)

    def test_sulfuras_does_not_change(self):
        items = [Item("Sulfuras, Hand of Ragnaros", 5, 80)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(80, items[0].quality)
        self.assertEqual(5, items[0].sell_in)

    def test_backstage_passes_increase_quality(self):
        items = [Item("Backstage passes to a TAFKAL80ETC concert", 15, 20)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(21, items[0].quality)

    def test_backstage_passes_increase_quality_by_two(self):
        items = [Item("Backstage passes to a TAFKAL80ETC concert", 10, 20)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(22, items[0].quality)

    def test_backstage_passes_increase_quality_by_three(self):
        items = [Item("Backstage passes to a TAFKAL80ETC concert", 5, 20)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(23, items[0].quality)

    def test_backstage_passes_quality_becomes_zero_after_concert(self):
        items = [Item("Backstage passes to a TAFKAL80ETC concert", 0, 20)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(0, items[0].quality)

    def test_conjured_item_quality_decreases_twice_as_fast(self):
        items = [Item("Conjured Mana Cake", 10, 20)]
        gilded_rose = GildedRose(items)
        gilded_rose.update_quality()

        self.assertEqual(18, items[0].quality)
        self.assertEqual(9, items[0].sell_in)


if __name__ == '__main__':
    unittest.main()